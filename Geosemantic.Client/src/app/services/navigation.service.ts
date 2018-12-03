import { Injectable } from '@angular/core';
import { Router } from "@angular/router";

@Injectable()

export class NavigationService {
  constructor(private router: Router) {

  }

  goToDashboard() {
    this.router.navigate(['/dashboard']);
  }

  goToCluster() {
    this.router.navigate(['/clusters']);
  }

  goToEmployees() {
    this.router.navigate(['/employees']);
  }
  goToUser() {
    this.router.navigate(['/users']);
  }

  goToUserDetails(id) {
    this.router.navigate(['/user/' + id]);
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }
   
  goToUnderRejected() {
    this.router.navigate(['/rejected']);
  }

  goToUnAuthorized() {
    this.router.navigate(['/unauthorized']);
  }

  goToSessionTimedOut() {
    this.router.navigate(['/sessiontimedout']);
  }

  goToOrganisation() {
    this.router.navigate(['/organisation']);
  }

  isOnLoginScreen():boolean {
    return this.router.url === '/login';
  };
}
