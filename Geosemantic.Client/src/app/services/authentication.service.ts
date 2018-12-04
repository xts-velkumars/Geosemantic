import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserSessionService } from "./usersession.service";
import { DataService } from "./data.service";
import { UserSession } from "../models";
import { UserPageSessionService } from './userpagesession.service';

import * as jwtDecode from "jwt-decode";
import * as momenttz from 'moment-timezone';
import * as _ from "lodash";

import 'rxjs/add/operator/map';
import { OrganisationPageSessionService } from './organisationpagesession.service';
declare var require: any;  
var timezone = require('../../assets/timezones.json');

@Injectable()
export class AuthenticationService {

  private baseUrl = environment.apiBaseUrl;
  timeZones: any[];
  sessionData = new UserSession();

  constructor(
    private http: HttpClient,
    private dataService: DataService,
    private sessionService: UserSessionService,
    private organisationPageSessionService:OrganisationPageSessionService,
    private userPageSessionService: UserPageSessionService) {
      this.getTimeZones();
  }

  login(username: string, password: string) {

    // username='9600155567';
    // password='reset@123';

    // username='9789978997';
    // password='bash@123';
    
    const headers = new HttpHeaders(
      {
        'Content-Type': 'application/x-www-form-urlencoded',
      });

   const timeZone = this.getBrowserTimeZone();
    const data = 'username=' + username + '&password=' + password + '&timezone=' + timeZone;

    return this.http.post<any>(this.baseUrl + "/api/token", data, { headers: headers })
    .map(user => {
      debugger;
      if (user && user.access_token) {
         this.clearCachedMenu();
         const decodedToken = jwtDecode(user.access_token);
         this.sessionData.authToken = user.access_token;
         this.sessionData.userId = decodedToken["user.id"];
         this.sessionData.userFullName = decodedToken["user.fullname"];
         this.sessionData.userStatus = decodedToken["referrence1"];
         this.sessionData.userroleid= decodedToken["user.roleid"];
         this.sessionData.userrolename= decodedToken["user.rolename"];
         this.sessionService.create(this.sessionData);
      }
      
      return user;
    });
  }

  isAuthenticated() {
    return !!this.sessionService.userId() && !!this.sessionService.authToken();
  };

  hasRequiredPermission(permission) {
    for (var i = 0; i < permission.length; i++) {
      if (permission[i] === this.sessionService.roleId())
        return true;
    }
    return false;
  };

  getTimeZones() {
    this.timeZones = timezone.timeZone;
  }

  getBrowserTimeZone(): string {
    var zoneName = momenttz.tz.guess();
    var timezone = momenttz.tz(zoneName).zoneAbbr();
    var filterZone = this.timeZones.find(i => i.abbr === timezone);
    if (filterZone) {
      return filterZone.value;
    }
    return '';
  }

  logOut() {
    this.clearCachedMenu();
    this.sessionService.destroy();
  }

  clearCachedMenu() {
    this.dataService.clearCache();
    this.organisationPageSessionService.destroy();
    this.userPageSessionService.destroy();
  }

  

  
}
