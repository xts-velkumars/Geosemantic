import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTabChangeEvent } from '@angular/material';
import { DataSource, SelectionModel } from '@angular/cdk/collections';
import { merge, Observable, BehaviorSubject, fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { FuseUtils } from '@fuse/utils';
import { takeUntil } from 'rxjs/internal/operators';
 
 
import { MatDialog, MatDialogConfig, MatDialogRef } from "@angular/material";
import { ConfirmationModalComponent } from '../../shared/component/modalcomponent/confirmationmodal.component';
import { FormGroup } from '@angular/forms';
import { AlertService } from '../../services';
import { UserService } from '../../services/userdata.service';

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
    usersColumns = ['firstName', 'lastName', 'emailAddress', 'mobileNumber', 'roleName', 'actions', 'delete'];
    rolesColumns = ['selectRole', 'id', 'name', 'actions'];
    userSelection = new SelectionModel<any>(true, []);
    roleSelection = new SelectionModel<any>(true, []);
    
    dialogRef: any;

    @ViewChild(MatPaginator)
    paginator: MatPaginator;

    @ViewChild(MatSort)
    sort: MatSort;

    @ViewChild('filter')
    filter: ElementRef;

    private unsubscribeAll: Subject<any>;

    constructor(private usersService: UserService,
        private dialog: MatDialog,
        private alertService: AlertService, ) {
        this.unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        this.getUsers(true);
    }

    approvedUser(user) {


        const dialogConfig = new MatDialogConfig();
        dialogConfig.autoFocus = true;
        dialogConfig.data = {
            title: 'Approved Confirmation',
            body: 'Are you sure you want to approved the user?',
        };

        const dialogRef = this.dialog.open(ConfirmationModalComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(result => {
            if (result === true) {
                this.usersService.approved(user.id).subscribe(users => {
                    this.alertService.success("User Status Updated successfully");
                    this.refresh();
                });
            }
        });


       
    }


    refresh() {
        this.getUsers(true);
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

    onDelete(user) {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.autoFocus = true;
        dialogConfig.data = {
            title: 'Delete Confirmation',
            body: 'Are you sure you want to delete the user?',
        };

        const dialogRef = this.dialog.open(ConfirmationModalComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(result => {
            if (result === true) {
                this.usersService.deleteUser(user.id).subscribe(users => {
                    this.alertService.success("User Deleted successfully");
                    this.refresh();
                });
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
                case 'firstName':
                    [propertyA, propertyB] = [a.firstName, b.firstName];
                    break;
                case 'lastName':
                    [propertyA, propertyB] = [a.lastName, b.lastName];
                    break;
                case 'mobileNumber':
                    [propertyA, propertyB] = [a.mobileNumber, b.mobileNumber];
                    break;
                case 'roleName':
                    [propertyA, propertyB] = [a.roleName, b.roleName];
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

