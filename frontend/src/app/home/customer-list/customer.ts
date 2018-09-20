export default class Customer {
  public fullname: string;
  public address: string;
  public phone: string;
  public email: string;
  public status: StatusEnum;
}

export enum StatusEnum {
  prospective = 1,
  current = 2,
  nonActive = 3
}
