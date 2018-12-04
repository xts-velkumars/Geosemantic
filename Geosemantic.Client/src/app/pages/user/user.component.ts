import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators,ValidatorFn,AbstractControl,ValidationErrors } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { AlertService } from 'app/services/alert.service';
import { UserService } from 'app/pages/user/user.service';
import { NavigationService } from 'app/services/navigation.service';
import { UtilityService, OrganisationPageSessionService } from '../../services';
import { Register } from 'app/models/register';

@Component({
    selector     : 'user-dashboard',
    templateUrl  : './user.component.html',
    styleUrls    : ['./user.component.scss'],
    animations   : fuseAnimations
})
export class UserComponent implements OnInit
{
    routeParams: any;
    user: any;
    form: FormGroup;
    id=0;
    cardTitle;
    action;
    genderTypes:any;
    roleTypes:any;
    register :Register;
    private orgId=this.organisationPageSessionService.getOrganisationId();
    
   
    constructor(private route: ActivatedRoute,
        private alertService: AlertService,
        private navigationService: NavigationService,
        private utilityService:UtilityService,
        private organisationPageSessionService:OrganisationPageSessionService,
        private userService: UserService, private formBuilder: FormBuilder
    )
    {
       
       this.routeParams = route.snapshot.params;
       this.id=this.routeParams.id;
       this.cardTitle=this.id==0?"User Add":"User Edit";
       this.action=this.id==0?"Add":"Save";
    }
    
    ngOnInit(): void
    {
        this.form = this.formBuilder.group({
            firstName : ['', Validators.required],
            lastName  : ['', Validators.required],
            mobileNumber: ['', Validators.compose([Validators.required, Validators.maxLength(10)])],
            emailAddress: ['', Validators.compose([Validators.required, Validators.email])],
            dateOfBirth : ['', Validators.required],
            genderType:['', Validators.required],
            roleId:['', Validators.required],
            password: ['', Validators.required],
            passwordConfirm: ['', [Validators.required, confirmPasswordValidator]],
            organisationId:['', null],
            id:[this.id,null]
        });
        this.getGenderType(false);
        this.getRoles(true);
        //this.getUser(true);
       
    }

    getGenderType(refresh) {
        this.utilityService.getGenderType(refresh).subscribe(genderTypes => {
          this.genderTypes = genderTypes;
        });
      }
    getUser(refresh) {
        if(this.id==0) return;
        
        this.userService.getUser(this.id,this.orgId,refresh).subscribe(user => {
          this.user = user;
          this.form.patchValue(this.user);
        });
      }

      getRoles(refresh) {
        this.userService.getRoles(this.orgId,refresh).subscribe(roles => {
            this.roleTypes = roles;
        });
    }

      save() {
        if (this.form.valid) {
          debugger;
          this.userService.saveUser(this.register).subscribe(data => {
              debugger;
            this.alertService.success("User saved successfully");
            this.navigationService.goToUser();
          });
        }
      }
}

export const confirmPasswordValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {

  if (!control.parent || !control) {
      return null;
  }

  const password = control.parent.get('password');
  const passwordConfirm = control.parent.get('passwordConfirm');

  if (!password || !passwordConfirm) {
      return null;
  }

  if (passwordConfirm.value === '') {
      return null;
  }

  if (password.value === passwordConfirm.value) {
      return null;
  }

  return { 'passwordsNotMatching': true };
};



