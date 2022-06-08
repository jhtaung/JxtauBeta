import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Address } from '../models/address';
import { EformUserDetailDto } from '../models/eform-user-detail';
import { EformResponse } from '../models/eform-user-list';

@Injectable({
  providedIn: 'root',
})
export class EformService {
  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUserList() {
    return this.http.get<EformResponse>(this.baseUrl + 'Eforms/Users/List');
  }

  getUser(id: string) {
    return this.http.get<EformUserDetailDto>(this.baseUrl + 'Eforms/Users/' + id);
  }

  deleteUser(id: string) {
    return this.http.delete<any>(this.baseUrl + 'Eforms/Users/' + id);
  }

  getAddress(address: Address) {
    return this.http.post<Address>(this.baseUrl + 'Address/Validate', address);
  }

  getEforms() {
    return this.http.get('assets/data/eforms.txt', { responseType: 'text' });
  }

  getEformsBot() {
    return this.http.get('assets/data/eforms-bot.txt', { responseType: 'text' });
  }
}
