import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators,FormControl} from '@angular/forms';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { AuthenticationService, UserSessionService } from 'app/services';
import { Login } from 'app/models';
import { Router, ActivatedRoute } from '@angular/router';
import { FuseNavigationService } from '../../../@fuse/components/navigation/navigation.service';
import { OrganisationService } from '../organisation/organisation.service';
import { OrganisationPageSessionService } from '../../services/organisationpagesession.service';

@Component({
    selector     : 'login',
    templateUrl  : './login.component.html',
    styleUrls    : ['./login.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class LoginComponent implements OnInit
{
    loginForm: FormGroup;
    vm: Login;
    returnUrl: string;
    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FormBuilder} _formBuilder
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private router: Router,
    private route: ActivatedRoute,
    private authService: AuthenticationService,
    private userSessionService:UserSessionService,
    private organisationService:OrganisationService,
    private organisationPageSessionService:OrganisationPageSessionService,
    private fuseNavigationService:FuseNavigationService)
    {
        // Configure the layout
        this._fuseConfigService.config = {
            layout: {
                navbar   : {
                    hidden: true
                },
                toolbar  : {
                    hidden: true
                },
                footer   : {
                    hidden: true
                },
                sidepanel: {
                    hidden: true
                }
            }
        };

        this.router = router;
        this.vm = new Login();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
        this.loginForm = this._formBuilder.group({
            mobileNumber   : ['', Validators.compose([Validators.required, Validators.maxLength(10)])],
            password: ['', Validators.required]
        });
    }

    login() {
        debugger;
        if (this.loginForm.valid) {
            this.authService.login(this.vm.username, this.vm.password).subscribe(() => {
                this.organisation();
            });
        }
        else {
          //this.validateAllFormFields(this.loginForm);
        }
      }
      loginStatic(username,password){
        this.authService.login(username,password).subscribe(() => {
            this.organisation();
        });
      }

      organisation(){
          let isMultipleOrganisation=this.userSessionService.isMultipleOrganisation();
        if (!isMultipleOrganisation){
            let userId=this.userSessionService.userId();
                this.organisationService.getOrganisation(userId,true).subscribe(organisation=>{
                    this.organisationPageSessionService.create(organisation[0]);
                    this.fuseNavigationService.getNav();
                    this.proceedDashboard();
                });
        }else{
            this.proceedOrganisation();
        }
      }

      proceedDashboard() {
          debugger;
        this.router.navigate([this.returnUrl]);
      }

      proceedOrganisation() {
          debugger;
        this.router.navigate(['/organisation']);
      }
}
