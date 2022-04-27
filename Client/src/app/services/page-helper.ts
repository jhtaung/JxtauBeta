import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { PageResult } from '../models/page-params';

export function getPageResult<T>(
  http: HttpClient,
  url: string,
  params: HttpParams
) {
  const pageResult: PageResult<T> = new PageResult<T>();
  return http.get<T>(url, { observe: 'response', params }).pipe(
    map((response) => {
      pageResult.result = response.body!;
      if (response.headers.get('Pagination') !== null) {
        pageResult.page = JSON.parse(response.headers.get('Pagination')!);
      }
      return pageResult;
    })
  );
}

export function getPageHeaders(pageNumber: number, pageSize: number) {
  let params = new HttpParams();

  params = params.append('pageNumber', pageNumber.toString());
  params = params.append('pageSize', pageSize.toString());

  return params;
}
