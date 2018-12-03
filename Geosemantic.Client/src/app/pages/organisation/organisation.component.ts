import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators,FormControl} from '@angular/forms';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { AuthenticationService, UserSessionService, NavigationService } from 'app/services';
import { Router, ActivatedRoute } from '@angular/router';
import {OrganisationService} from '../organisation/organisation.service';
import { FuseNavigationService } from '../../../@fuse/components/navigation/navigation.service';
import { OrganisationPageSessionService } from '../../services/organisationpagesession.service';

@Component({
    selector     : 'organisation',
    templateUrl  : './organisation.component.html',
    styleUrls    : ['./organisation.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class OrganisationComponent implements OnInit
{
    orginizationTypes:any;
    form: FormGroup;
    constructor(
        private _fuseConfigService: FuseConfigService,
        private formBuilder: FormBuilder,
        private router: Router,
    private route: ActivatedRoute,
    private authService:AuthenticationService,
    private navigationService:NavigationService,
    private organisationService:OrganisationService,

    private organisationPageSessionService: OrganisationPageSessionService,
    private userSessionService:UserSessionService,
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
    }

   
    ngOnInit(): void
    {
        let userId=this.userSessionService.userId();
        this.form = this.formBuilder.group({
            orginizationType:['', Validators.required],
        });
        this.organisationService.getOrganisation(userId,true).subscribe(organisation=>{
            this.orginizationTypes=organisation;
        });
    }

    orginizationChange(e,organisation) 
    {
        this.organisationPageSessionService.create(organisation);
        this.fuseNavigationService.clearNav();
        this.fuseNavigationService.getNav();
        this.proceedDashboard();
    }
    proceedDashboard() {
        this.router.navigate(['/dashboard']);
      }

      onLogout() {
        this.authService.logOut();
        this.navigationService.goToLogin();
      }
}
