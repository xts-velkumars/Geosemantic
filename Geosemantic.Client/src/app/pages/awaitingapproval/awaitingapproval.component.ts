import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FuseConfigService } from '@fuse/services/config.service';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
@Component({
    selector     : 'awaiting-approval',
    templateUrl  : './awaitingapproval.component.html',
    styleUrls    : ['./awaitingapproval.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class AwaitingApprovalComponent implements OnInit, OnDestroy {
    constructor(
        private router: Router,
        private _fuseConfigService: FuseConfigService
      
    ) {
        
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

    }

    
    ngOnInit(): void {
        
    }

    login() {
        this.router.navigate(['/login']);
    }
   
    ngOnDestroy(): void {

    }



}






