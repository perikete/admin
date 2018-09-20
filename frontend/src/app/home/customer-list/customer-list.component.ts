import { Component, OnInit } from "@angular/core";
import { CustomerListService } from "./customer-list.service";
import { Observable } from "rxjs";
import Customer, { StatusEnum } from "./customer";

@Component({
  selector: "app-customer-list",
  templateUrl: "./customer-list.component.html",
  styleUrls: ["./customer-list.component.css"]
})
export class CustomerListComponent implements OnInit {
  
  public StatusEnum = StatusEnum;
  
  public customers: Observable<Customer[]>
  
  constructor(private _customerListService: CustomerListService) {}

  ngOnInit() {
    this.customers = this._customerListService.getCustomers();
  }
}
