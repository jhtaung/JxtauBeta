import { PageParams } from './page-params';

export class AppealParams extends PageParams {
  search: string = '';
  id: number | null = null;
  rap: string = '';
  dept: string = '';
  mpid: string = '';
  firstName: string = '';
  lastName: string = '';
  meeting: Date | null = null;
  status: string = '';
  updatedUser: string = '';
  updatedDate: Date | null = null;
  receivedDate: Date | null = null;
  notes: string = '';

  apiModel: any = {};

  build() {
    let apiModel = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      search: this.search,
    };

    this.id = Number(this.id);
    if (this.id === 0) {
      this.id = null;
    } else if (Number(this.id) !== NaN) {
      apiModel = { ...apiModel, ...{ id: { match: this.id } }};
    }

    if (this.rap !== '') {
      apiModel = { ...apiModel, ...{ rap: this.rap === 'true' } };
    }

    if (this.dept !== '') {
      apiModel = { ...apiModel, ...{ dept: this.dept } };
    }

    if (this.updatedDate !== null) {
      apiModel = { ...apiModel, ...{ updatedDate: this.updatedDate } };
    }

    if (this.mpid !== '') {
      apiModel = { ...apiModel, ...{ mpid: { match: this.mpid } }};
    }

    if (this.firstName !== '') {
      apiModel = { ...apiModel, ...{ firstName: this.firstName } };
    }

    if (this.lastName !== '') {
      apiModel = { ...apiModel, ...{ lastName: this.lastName } };
    }

    if (this.meeting !== null) {
      apiModel = { ...apiModel, ...{ meeting: this.meeting } };
    }

    if (this.status !== '') {
      apiModel = { ...apiModel, ...{ status: this.status } };
    }

    if (this.updatedUser !== '') {
      apiModel = { ...apiModel, ...{ statusUpdateUser: this.updatedUser } };
    }

    console.log(apiModel);
    this.apiModel = apiModel;
  }
}
