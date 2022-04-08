export interface AppealList {
  id: number;
  rap: boolean;
  dept: string;
  mpid: string;
  firstName: string;
  lastName: string;
  meeting: Date;
  status: string;
  notes: string;
  statusUpdateUser: string;
  statusUpdateDate: Date;
  receivedDate: Date;
}
