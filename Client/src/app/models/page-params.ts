export interface Page {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
}

export class PageResult<T> {
  result!: T;
  page!: Page;
}

export class PageParams {
  pageNumber: number = 1;
  pageSize: number = 10;
  orderBy: string = '';
  search: string = '';
}
