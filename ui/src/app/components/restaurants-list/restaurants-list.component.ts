import { Component, OnInit } from '@angular/core';
import {ConfigStaticService} from "../../shared/config.static.service";
import {RestaurantService} from "../../shared/restaurant.service";
import {NgRedux} from "@angular-redux/store";
import {IAppState} from '../../../store/store';
import {LoadRestaurants} from "../../../store/actions";

@Component({
  selector: 'restaurants-list',
  templateUrl: './restaurants-list.component.html',
  styleUrls: ['./restaurants-list.component.css']
})
export class RestaurantsListComponent implements OnInit {
  data: any[];
  settings = {
    actions: {
      add: true,
      edit: true,
      position: 'right'
    },
    columns: ConfigStaticService.tableConfig()
  };

  constructor(private restaurantService: RestaurantService,
              private ngRedux: NgRedux<IAppState>) {
    this.getAllRestaurants();
    this.ngRedux.subscribe(()=> {
      let state = this.ngRedux.getState();
      this.data = state.restaurants;
    });
  }

  getAllRestaurants = () => {
    this.restaurantService.GetAllRestaurants()
      .subscribe((data) => {
        this.ngRedux.dispatch(LoadRestaurants(data));
      });
  }

  ngOnInit(): void {
  }

}
