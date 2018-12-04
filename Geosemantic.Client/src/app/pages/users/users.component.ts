import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTabChangeEvent } from '@angular/material';
import { DataSource, SelectionModel } from '@angular/cdk/collections';
import { merge, Observable, BehaviorSubject, fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { FuseUtils } from '@fuse/utils';
import { takeUntil } from 'rxjs/internal/operators';
import { UsersService } from 'app/pages/users/users.service';
import { OrganisationPageSessionService } from '../../services/organisationpagesession.service';
import {MatDialog, MatDialogConfig,MatDialogRef} from "@angular/material";
import { ConfirmationModalComponent } from '../../shared/component/modalcomponent/confirmationmodal.component';
import { RoleComponent } from '../role/role.component';
import { FormGroup } from '@angular/forms';
import { RoleService } from '../role/role.service';
import { AlertService } from '../../services';

@Component({
    selector: 'users-dashboard',
    templateUrl: './users.component.html',
    styleUrls: ['./users.component.scss'],
    animations: fuseAnimations
})
export class UsersComponent implements OnInit {
    userDataSource: FilesUserDataSource | null;
    roleDataSource: FilesUserDataSource | null;
    users: any;
    roles: any;
    tabindex = 0;
    usersColumns = ['firstName', 'lastName','emailAddress','mobileNumber', 'roleName','actions'];
    rolesColumns = ['selectRole','id', 'name', 'actions'];
    userSelection = new SelectionModel<any>(true, []);
    roleSelection = new SelectionModel<any>(true, []);
    confirmDialogRef: MatDialogRef<RoleComponent>;
    dialogRef: any;

    @ViewChild(MatPaginator)
    paginator: MatPaginator;

    @ViewChild(MatSort)
    sort: MatSort;

    @ViewChild('filter')
    filter: ElementRef;

    private unsubscribeAll: Subject<any>;

    constructor(private usersService: UsersService,
        private organisationPageSessionService:OrganisationPageSessionService,
        private dialog: MatDialog,
        private roleService: RoleService,
        private alertService: AlertService,) {
        this.unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        this.getUsers(true);     
    }

    activeStatus(user) {
        user.isActive = !user.isActive ? true : false;
    }

    getUsers(refresh) {
        
        this.usersService.getUsers(refresh).subscribe(users => {
            this.users = users;
            this.userDataSource = new FilesUserDataSource(this.paginator, this.sort, this.users);
            fromEvent(this.filter.nativeElement, 'keyup')
                .pipe(
                    takeUntil(this.unsubscribeAll),
                    debounceTime(150),
                    distinctUntilChanged()
                )
                .subscribe(() => {
                    if (!this.userDataSource) {
                        return;
                    }
                    this.userDataSource.filter = this.filter.nativeElement.value;
                });
        });
    }

    
    ngOnDestroy(): void {
        this.unsubscribeAll.next();
        this.unsubscribeAll.complete();
    }

    isAllSelectedUser() {
        const numSelectedUser = this.userSelection.selected.length;
        const numRowsUser = this.userDataSource.filteredData.length;
        return numSelectedUser === numRowsUser;
    }

    isUserDeleteDisable() {
        return this.userSelection.selected.length == 0;
    }
    userMasterToggle() {
        this.isAllSelectedUser() ?
            this.userSelection.clear() :
            this.userDataSource.filteredData.forEach(row => this.userSelection.select(row));
    }

    isAllSelectedRole() {
        const numSelected = this.roleSelection.selected.length;
        const numRows = this.roleDataSource.filteredData.length;
        return numSelected === numRows;
    }

    isRoleDeleteDisable() {
        return this.roleSelection.selected.length == 0;
    }
    roleMasterToggle() {
        this.isAllSelectedRole() ?
            this.roleSelection.clear() :
            this.roleDataSource.filteredData.forEach(row => this.roleSelection.select(row));
    }

    onTabClick(event: MatTabChangeEvent) {
        console.log('index => ', event.index);
        this.tabindex = event.index;
        console.log('tab => ', event.tab.textLabel);
    }

    delete() {

        const dialogConfig = new MatDialogConfig();

        dialogConfig.autoFocus = true;
        dialogConfig.data = {
            title: 'Delete Confirmation',
            body: 'Are you sure you want to delete the selected users?',
        };


        const dialogRef = this.dialog.open(ConfirmationModalComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(result => {
            if (result === true) {
             
            }
          });    
    }

    actionRole(actionName,id): void
    {
        this.dialogRef = this.dialog.open(RoleComponent, {
            panelClass: 'contact-form-dialog',
            data      : {
                id: id,
                action : actionName,
            }
        });

        this.dialogRef.afterClosed()
            .subscribe(response => {
                if ( !response )
                {
                    return;
                }
                const actionType: string = response[0];
                const roleForm: FormGroup = response[1];
                switch ( actionType )
                {
                    case 'save':
                        roleForm.value.organisationId=this.organisationPageSessionService.getOrganisationId();
                        this.roleService.saveRole(roleForm.value).subscribe(data => {
                        this.alertService.success("Role saved successfully");                        
                        });
                        break;
                    // case 'delete':
                    //     this.deleteContact(contact);
                    //     break;
                }
            });
    }

    

}

export class FilesUserDataSource extends DataSource<any>
{
    private filterChange = new BehaviorSubject('');
    private filteredDataChange = new BehaviorSubject('');
    onProductsChanged: BehaviorSubject<any>;

    constructor(private matPaginator: MatPaginator,
        private matSort: MatSort,
        private data: any) {
        super();
        this.onProductsChanged = new BehaviorSubject({});
        this.filteredData = this.data;
        this.onProductsChanged.next(this.data);
    }
    connect(): Observable<any[]> {
        const displayDataChanges = [
            this.onProductsChanged,
            this.matPaginator.page,
            this.filterChange,
            this.matSort.sortChange
        ];

        return merge(...displayDataChanges)
            .pipe(
                map(() => {
                    let data = this.data.slice();
                    data = this.filterData(data);
                    this.filteredData = [...data];
                    data = this.sortData(data);
                    const startIndex = this.matPaginator.pageIndex * this.matPaginator.pageSize;
                    return data.splice(startIndex, this.matPaginator.pageSize);
                }
                ));
    }

    get filteredData(): any {
        return this.filteredDataChange.value;
    }

    set filteredData(value: any) {
        this.filteredDataChange.next(value);
    }

    get filter(): string {
        return this.filterChange.value;
    }

    set filter(filter: string) {

        this.filterChange.next(filter);
    }

    filterData(data): any {
        if (!this.filter) {
            return data;
        }
        return FuseUtils.filterArrayByString(data, this.filter);
    }

    sortData(data): any[] {
        if (!this.matSort.active || this.matSort.direction === '') {
            return data;
        }

        return data.sort((a, b) => {
            let propertyA: number | string = '';
            let propertyB: number | string = '';
            switch (this.matSort.active) {
                case 'emailAddress':
                    [propertyA, propertyB] = [a.emailAddress, b.emailAddress];
                    break;
                case 'fullName':
                    [propertyA, propertyB] = [a.fullName, b.fullName];
                    break;
                case 'mobileNumber':
                    [propertyA, propertyB] = [a.mobileNumber, b.mobileNumber];
                    break;
                case 'name':
                    [propertyA, propertyB] = [a.name, b.name];
                    break;
            }

            const valueA = isNaN(+propertyA) ? propertyA : +propertyA;
            const valueB = isNaN(+propertyB) ? propertyB : +propertyB;

            return (valueA < valueB ? -1 : 1) * (this.matSort.direction === 'asc' ? 1 : -1);
        });
    }

    disconnect(): void {
    }
}

