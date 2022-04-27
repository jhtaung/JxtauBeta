export interface EformUserDetailDto {
  id: string;
  username: string;
  firstName: string;
  lastName: string;
  disabled: boolean;
  theme: string;
  changePasswordAtNextLogin: boolean;
  passwordNeverExpires: boolean;
  lockedOut: boolean;
  twoFactorAuth: boolean;
  email: string;
  culture: string;
  language: string;
  timezone: string;
  prefix: string;
  jobTitle: string;
  organization: string;
  phone: string;
  fax: string;
  pictureUrl: string;
  streetAddress: StreetAddress;
  postalAddress: PostalAddress;
  strictAccessibility: boolean;
  customFields: CustomFields;
  roles: string[];
  groups: string[];
}

export interface StreetAddress {
  line1: string;
  line2: string;
  city: string;
  state: string;
  zipcode: string;
  country: string;
}

export interface PostalAddress {
  line1: string;
  line2: string;
  city: string;
  state: string;
  zipcode: string;
  country: string;
}

export interface CustomFields {
  empID: string;
  mPIID: string;
}
