export interface EformResponse {
  data: EformUserDto[] | null;
  nextLink: string | null;
}

export interface EformUserDto {
  id: string;
  username: string;
  firstName: string;
  lastName: string;
  disabled: boolean;
  log: string;
}
