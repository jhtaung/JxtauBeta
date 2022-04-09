import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AppealList } from '../models/appeal-list';
import { AppealParams } from '../models/appeal-params';
import { getPageHeaders, getPageResult } from './page-helper';

@Injectable({
  providedIn: 'root',
})
export class AppealService {
  baseUrl: string = environment.apiUrl;
  appealParams: AppealParams = new AppealParams();
  constructor(private httpClient: HttpClient) {}

  getAppealParams() {
    return this.appealParams;
  }

  setAppealParams(params: AppealParams) {
    this.appealParams = params;
  }

  getAppealList(appealParams: AppealParams) {
    let params = getPageHeaders(appealParams.pageNumber, appealParams.pageSize);
    params = appealParams.orderBy == "" ? params : params.append('orderby', appealParams.orderBy);
    params = appealParams.search == "" ? params : params.append('search', appealParams.search);
    return getPageResult<AppealList[]>(
      this.httpClient,
      this.baseUrl + 'Appeals/List',
      params
    ).pipe(
      map((response) => {
        response.result.map((x) => {
          x.meeting = new Date(x.meeting);
          return x;
        });
        return response;
      })
    );
  }
}
