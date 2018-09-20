import { Component, OnInit } from '@angular/core';
import { DetailsService } from './details.service';
import { Observable } from 'rxjs';
import Customer, { StatusEnum } from '../customer-list/customer';
import {
  ActivatedRoute,
  ActivatedRouteSnapshot,
  Router
} from '@angular/router';

@Component({
  selector: 'app-customer-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  public customer: Observable<Customer>;
  public StatusEnum = StatusEnum;

  constructor(
    private _detailsService: DetailsService,
    private _router: Router,
    private _route: ActivatedRoute
  ) {}

  ngOnInit() {
    const snapshot = this._route.snapshot;
    this.customer = this._detailsService.getCustomer(snapshot.params.id);
  }

  public back() {
    this._router.navigate(['/customers']);
  }
}
