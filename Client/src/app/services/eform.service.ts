import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { EformResponse } from '../models/eform-user-list';

@Injectable({
  providedIn: 'root'
})
export class EformService {
  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }
  
  getUserList () {
    return this.http.get<EformResponse>(this.baseUrl + "Eforms/Users/List").pipe(map(x => x));
  }
}
