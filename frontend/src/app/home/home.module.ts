import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HomeComponent } from "./home.component";
import { CustomerListComponent } from "./customer-list/customer-list.component";

@NgModule({
  imports: [CommonModule],
  declarations: [HomeComponent, CustomerListComponent],
  exports: [HomeComponent]  
})
export class HomeModule {}
