import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { AuthenticationService, UserSessionService } from 'app/services';
import { Login } from 'app/models';
import { Router, ActivatedRoute } from '@angular/router';
import { FuseNavigationService } from '../../../@fuse/components/navigation/navigation.service';


@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    vm: Login;
    returnUrl: string;
    status: string;
    
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private authService: AuthenticationService,
        private userSessionService: UserSessionService,
        private fuseNavigationService: FuseNavigationService) {

        this._fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: true
                },
                toolbar: {
                    hidden: true
                },
                footer: {
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

    
    ngOnInit(): void {
        
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
        this.loginForm = this._formBuilder.group({
            email: ['', Validators.compose([Validators.required, Validators.email])],
            password: ['', Validators.required]
        });
    }

    login() {
        if (this.loginForm.valid) {
            this.authService.login(this.vm.username, this.vm.password).subscribe(() => {
                this.status = this.userSessionService.userStatus();
                if (this.status === 'Approved') {
                    this.router.navigate([this.returnUrl]);
                }
                else {
                    this.router.navigate(['/awaitingapproval']);
                }

            });
        }
    }
}
