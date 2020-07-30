import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {ConfigStaticService} from "../../shared/config.static.service";
import {RestaurantService} from "../../shared/restaurant.service";
import {NgRedux} from "@angular-redux/store";
import {IAppState} from '../../../store/store';
import {DeleteRestaurant, LoadRestaurants} from "../../../store/actions";
import {Restaurant} from "../../shared/restaurant";
import {Ng2SmartTableComponent} from "ng2-smart-table";
import * as _ from 'lodash';

@Component({
  selector: 'restaurants-list',
  templateUrl: './restaurants-list.component.html',
  styleUrls: ['./restaurants-list.component.css']
})
export class RestaurantsListComponent implements AfterViewInit {
  @ViewChild('table')
  smartTable: Ng2SmartTableComponent;
  selectedRestaurant: Restaurant;
  data: any[];
  settings = {
    hideSubHeader: true,
    edit: {
      editButtonContent: '<span><i class="fa fa-edit" title="Edit"></i></span>'
    },
    delete: {
      deleteButtonContent: '<span class="delete"><i class="fa fa-trash" title="Delete"></i></span>'
    },
    actions: {
      position: 'right'
    },
    mode: 'external',
    columns: ConfigStaticService.tableConfig()
  };

  constructor(private restaurantService: RestaurantService,
              private ngRedux: NgRedux<IAppState>) {
    this.getAllRestaurants();
    this.ngRedux.subscribe(()=> {
      let state = this.ngRedux.getState();
      this.data = _.orderBy(state.restaurants, ['id'], ['desc']);
    });
  }

  getAllRestaurants = () => {
    this.restaurantService.GetAllRestaurants()
      .subscribe((data) => {
        this.ngRedux.dispatch(LoadRestaurants(data));
      });
  }

  ngAfterViewInit(): void {
    this.smartTable.edit.subscribe( (selectedRow: any) => {
      this.selectedRestaurant = selectedRow.data;
    });
    this.smartTable.delete.subscribe( (selectedRow: any) => {
      this.ngRedux.dispatch(DeleteRestaurant(selectedRow.data));
      this.restaurantService.DeleteRestaurant(selectedRow.data.id).subscribe();
    });
  }

  addNewRestaurant = () => {
    this.selectedRestaurant = new Restaurant();
  }

  onFormCancel() {
    this.selectedRestaurant = undefined;
  }
}
