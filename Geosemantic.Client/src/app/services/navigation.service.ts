import { Injectable } from '@angular/core';
import { Router } from "@angular/router";

@Injectable()

export class NavigationService {
  constructor(private router: Router) {

  }

  goToDashboard() {
    this.router.navigate(['/dashboard']);
  }
 
  goToUser() {
    this.router.navigate(['/users']);
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }

  isOnLoginScreen():boolean {
    return this.router.url === '/login';
  };
}
