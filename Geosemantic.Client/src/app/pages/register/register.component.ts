import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/internal/operators';
import { AuthenticationService, UserSessionService, UtilityService } from 'app/services';
import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { Register } from 'app/models/register';
import { UserService } from "../../services/userdata.service";
import { Router } from '@angular/router';

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class RegisterComponent implements OnInit, OnDestroy {
    form: FormGroup;
    register: Register;
    id = 0;
    genderTypes: any;
    
    roleTypes: any;
    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private router: Router,
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private authService: AuthenticationService,
        private userSessionService: UserSessionService,
        private utilityService: UtilityService,
        private userService: UserService,
    ) {
        this.register = new Register();
        // Configure the layout
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

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this.register.id = 0;
        this.register.roleId = 1;
        this.form = this._formBuilder.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            mobileNumber: ['', Validators.compose([Validators.required, Validators.maxLength(10)])],
            emailAddress: ['', Validators.compose([Validators.required, Validators.email])],
            dateOfBirth: ['', Validators.required],
            genderType: ['', Validators.required],
            roleId: ['', Validators.required],
            id: [this.id, null],
            password: ['', Validators.required],
            confirmpassword: ['', [Validators.required, confirmPasswordValidator]],

        }

        );
        this.getGenderType(false);
        this.getRoles(true);

        // Update the validity of the 'passwordConfirm' field
        // when the 'password' field changes
        this.form.get('password').valueChanges
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {
                this.form.get('confirmpassword').updateValueAndValidity();
            });
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    /**
 * On Register
 */
    onRegister() {
        if (this.form.valid) {
            this.userService.saveUser(this.form.value).subscribe(data => {
                this.router.navigate(["/underreview"]);
            });
        }
    }
    

    getGenderType(refresh) {
        this.utilityService.getGenderType(refresh).subscribe(genderTypes => {
            this.genderTypes = genderTypes;
        });
    }

    getRoles(refresh) {
        this.userService.getRoles(this.register.roleId, refresh).subscribe(roles => {
            this.roleTypes = roles;
        });
    }
}

/**
 * Confirm password validator
 *
 * @param {AbstractControl} control
 * @returns {ValidationErrors | null}
 */
export const confirmPasswordValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {

    if (!control.parent || !control) {
        return null;
    }

    const password = control.parent.get('password');
    const passwordConfirm = control.parent.get('confirmpassword');

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
