import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort ,MatTabChangeEvent} from '@angular/material';
import { DataSource, SelectionModel } from '@angular/cdk/collections';
import { merge, Observable, BehaviorSubject, fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';


import { fuseAnimations } from '@fuse/animations';
import { FuseUtils } from '@fuse/utils';
import { takeUntil } from 'rxjs/internal/operators';


import {  ContactsService } from 'app/pages/contacts/contacts.service';
import { OrganisationPageSessionService } from '../../services';

@Component({
    selector     : 'contacts-dashboard',
    templateUrl  : './contacts.component.html',
    styleUrls    : ['./contacts.component.scss'],
    animations   : fuseAnimations
})
export class ContactsComponent implements OnInit
{
    dataSource: FilesDataSource | null;
    contacts: any;
    paginatorIndex=0;
    tabindex=0;
    displayedColumns = ['select','fullName','emailAddress','mobileNumber','actions'] ;
    selection =new SelectionModel<any>(true, []);
    @ViewChild(MatPaginator)
    paginator: MatPaginator;

    @ViewChild(MatSort)
    sort: MatSort;

    @ViewChild('filter')
    filter: ElementRef;

    private orgId=this.organisationPageSessionService.getOrganisationId();

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {UsersService} _usersService
     */
    constructor(
        private contactsService: ContactsService,private organisationPageSessionService:OrganisationPageSessionService
    )
    {
       // Set the private defaults
       this._unsubscribeAll = new Subject();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
        // Get the Users from the service
        this.getUsers(true);
        

       
    }
    activeStatus(user){
        user.isActive=!user.isActive?true:false; 
    }
   
    getUsers(refresh) {
        this.contactsService.getUsers(this.orgId,refresh).subscribe(contacts => {
          this.contacts = contacts;
          this.dataSource = new FilesDataSource( this.paginator, this.sort,this.contacts);
          fromEvent(this.filter.nativeElement, 'keyup')
            .pipe(
                takeUntil(this._unsubscribeAll),
                debounceTime(150),
                distinctUntilChanged()
            )
            .subscribe(() => {
                if ( !this.dataSource )
                {
                    return;
                }
                this.dataSource.filter = this.filter.nativeElement.value;
            });
        });
      }

      /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    isAllSelected() {
        const numSelected = this.selection.selected.length;
        const numRows = this.dataSource.filteredData.length;
        return numSelected === numRows;
      }

      isDeleteDisable(){
        return this.selection.selected.length==0;
      }
      masterToggle() {
        this.isAllSelected() ?
            this.selection.clear() :
            this.dataSource.filteredData.forEach(row => this.selection.select(row));
      }

      onTabClick(event: MatTabChangeEvent) {
        console.log('index => ', event.index);
        this.tabindex=event.index;
        console.log('tab => ', event.tab.textLabel);
      }

}

export class FilesDataSource extends DataSource<any>
{
    private _filterChange = new BehaviorSubject('');
    private _filteredDataChange = new BehaviorSubject('');
    onProductsChanged: BehaviorSubject<any>;
    /**
     * Constructor
     *
     * @param {MatPaginator} _matPaginator
     * @param {MatSort} _matSort
     */
    constructor(
        private _matPaginator: MatPaginator,
        private _matSort: MatSort,
        private _users: any
    )
    {
        super();
        this.onProductsChanged = new BehaviorSubject({});
        this.filteredData = this._users;
        this.onProductsChanged.next(this._users);
    }

    /**
     * Connect function called by the table to retrieve one stream containing the data to render.
     *
     * @returns {Observable<any[]>}
     */
    connect(): Observable<any[]>
    {
        const displayDataChanges = [
            this.onProductsChanged,
            this._matPaginator.page,
            this._filterChange,
            this._matSort.sortChange
        ];

        return merge(...displayDataChanges)
            .pipe(
                map(() => {

                        let data = this._users.slice();

                        data = this.filterData(data);

                        this.filteredData = [...data];

                        data = this.sortData(data);

                        // Grab the page's slice of data.
                        const startIndex = this._matPaginator.pageIndex * this._matPaginator.pageSize;
                       // 
                        return data.splice(startIndex, this._matPaginator.pageSize);
                    }
                ));
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    // Filtered data
    get filteredData(): any
    {
        return this._filteredDataChange.value;
    }

    set filteredData(value: any)
    {
        this._filteredDataChange.next(value);
    }

    // Filter
    get filter(): string
    {
        return this._filterChange.value;
    }

    set filter(filter: string)
    {
      
        this._filterChange.next(filter);
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Filter data
     *
     * @param data
     * @returns {any}
     */
    filterData(data): any
    {
        if ( !this.filter )
        {
            return data;
        }
        return FuseUtils.filterArrayByString(data, this.filter);
    }

    /**
     * Sort data
     *
     * @param data
     * @returns {any[]}
     */
    sortData(data): any[]
    {
        if ( !this._matSort.active || this._matSort.direction === '' )
        {
            return data;
        }

        return data.sort((a, b) => {
            let propertyA: number | string = '';
            let propertyB: number | string = '';
//['emailAddress','firstName','lastName','mobileNumber','genderTypeName',roleName] 
            switch ( this._matSort.active )
            {
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
                case 'genderTypeName':
                    [propertyA, propertyB] = [a.genderTypeName, b.genderTypeName];
                    break;
                case 'roleName':
                    [propertyA, propertyB] = [a.roleName, b.roleName];
                    break;
            }

            const valueA = isNaN(+propertyA) ? propertyA : +propertyA;
            const valueB = isNaN(+propertyB) ? propertyB : +propertyB;

            return (valueA < valueB ? -1 : 1) * (this._matSort.direction === 'asc' ? 1 : -1);
        });
    }

    /**
     * Disconnect
     */
    disconnect(): void
    {
    }

    
}

