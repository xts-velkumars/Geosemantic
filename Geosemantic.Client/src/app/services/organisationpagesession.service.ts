import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';

import * as _ from "lodash";

@Injectable()

export class OrganisationPageSessionService {

  localStorageSessionKey: string;
  constructor() {
    this.localStorageSessionKey = 'React-' + environment.apiBaseUrl + '-OrganisationPages';
  }

  create(Organisation) {
    this.setLocalStorageProperties(Organisation);
  };

  destroy() {
    this.setLocalStorageProperties('');
  };

  load() { 
    const jsonData = localStorage.getItem(this.localStorageSessionKey);
    return jsonData;
  };


  getOrganisation() {
    const jsonData = localStorage.getItem(this.localStorageSessionKey);
    return jsonData == null ? '' : JSON.parse(jsonData);
  }

  getOrganisationId() {
    const jsonData = localStorage.getItem(this.localStorageSessionKey);
    if(jsonData != null) console.log("JSON.parse(jsonData).id-->"+JSON.parse(jsonData).id);
    return jsonData == null ? '' : JSON.parse(jsonData).id;
  }
  getOrganisationName() {
    const jsonData = localStorage.getItem(this.localStorageSessionKey);
    return jsonData == null ? '' : JSON.parse(jsonData).name;
  }

  isOrganisationCached(): boolean {
    const jsonData = localStorage.getItem(this.localStorageSessionKey);
    return !_.isEmpty(JSON.parse(jsonData));
  }

  setLocalStorageProperties(Organisation) {
    localStorage.setItem(this.localStorageSessionKey, JSON.stringify(Organisation));
  };
} 
