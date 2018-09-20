import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { CustomerComponent } from "./customer.component";
import { CustomerListComponent } from "./customer-list/customer-list.component";
import { CustomerRoutingModule } from "./customer-routing.module";
import { DetailsComponent } from "./details/details.component";

@NgModule({
  imports: [CommonModule, CustomerRoutingModule],
  declarations: [CustomerComponent, CustomerListComponent, DetailsComponent],
  exports: [CustomerComponent]
})
export class CustomerModule {}
