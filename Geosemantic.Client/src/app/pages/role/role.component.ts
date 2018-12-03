import { Component, Inject,OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { AlertService } from 'app/services/alert.service';
import { RoleService } from 'app/pages/role/role.service';
import { NavigationService } from 'app/services/navigation.service';
import {OrganisationPageSessionService } from '../../services';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
    selector     : 'role',
    templateUrl  : './role.component.html',
    styleUrls    : ['./role.component.scss'],
    animations   : fuseAnimations
})
export class RoleComponent implements OnInit
{
    routeParams: any;
    role: any;
    roleForm: FormGroup;
    id=0;
    cardTitle;
    roleTypes:any;
    action: string;
    dialogTitle: string;
    
   
    constructor(private route: ActivatedRoute,
        private alertService: AlertService,
        public matDialogRef: MatDialogRef<RoleComponent>,
        private navigationService: NavigationService,
        @Inject(MAT_DIALOG_DATA) private dialogData: any,
        private organisationPageSessionService:OrganisationPageSessionService,
        private roleService: RoleService, private formBuilder: FormBuilder
    )
    {
       //this.routeParams = route.snapshot.params;
      // this.id=this.routeParams.id;
       this.cardTitle=this.id==0?"Add":"Edit";
       this.action=this.id==0?"Add":"Save";

       this.action = dialogData.action;

        if ( this.action === 'edit' )
        {
            this.dialogTitle = 'Edit Role';
            this.id = dialogData.id;
        }
        else
        {
            this.dialogTitle = 'Add Role';
            this.id = dialogData.id;
        }
    }
    
    ngOnInit(): void
    {
        this.roleForm = this.formBuilder.group({
            name : ['', Validators.required],
            organisationId:['', null],
            id:[this.id,null]
        });
        this.getRole(true);
       
    }
    
      getRole(refresh) {
        if(this.id==0) return;
        this.roleService.getRole(this.id,refresh).subscribe(role => {
          this.role = role;
          this.roleForm.patchValue(this.role);
        });
      }

      save() {
        if (this.roleForm.valid) {
          this.roleForm.value.organisationId=this.organisationPageSessionService.getOrganisationId();
          this.roleService.saveRole(this.roleForm.value).subscribe(data => {
            this.alertService.success("Role saved successfully");
            this.navigationService.goToUser();
          });
        }
      }
}



