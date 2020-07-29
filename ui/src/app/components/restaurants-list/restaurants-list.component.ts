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
    hideSubHeader: true,
    add: {
      addButtonContent: '<i class="nb-plus"></i>',
      createButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
    },
    edit: {
      editButtonContent: '<span>Edit</span>',
      saveButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
    },
    delete: {
      deleteButtonContent: '<i class="nb-trash"></i>',
      confirmDelete: true,
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
