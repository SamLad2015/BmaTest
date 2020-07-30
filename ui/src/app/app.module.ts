import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

/* Http client module */
import { HttpClientModule } from '@angular/common/http';

/* Service */
import { RestaurantService } from './shared/restaurant.service';

/* Forms module */
import {FormsModule, ReactiveFormsModule} from '@angular/forms';


/* Components */
import { RestaurantsListComponent } from './components/restaurants-list/restaurants-list.component';
import {Ng2SmartTableModule} from "ng2-smart-table";
import {CheckboxComponent} from "./components/check-box.component";
import { NgRedux, NgReduxModule } from '@angular-redux/store';
import { IAppState, rootReducer, INITIAL_STATE } from '../store/store';
import { RestaurantComponent } from './components/restaurants-list/restaurant/restaurant.component';
import {FormBuilderStaticService} from "./shared/formBuilder.static.service";
import { FiltersComponent } from './components/restaurants-list/filters/filters.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    RestaurantsListComponent,
    CheckboxComponent,
    RestaurantComponent,
    FiltersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    Ng2SmartTableModule,
    NgReduxModule,
    FormsModule,
    BrowserAnimationsModule
  ],
  providers: [RestaurantService, FormBuilderStaticService],
  bootstrap: [AppComponent]
})

export class AppModule {
  constructor (ngRedux: NgRedux<IAppState>) {
    ngRedux.configureStore(rootReducer, INITIAL_STATE);
  }
}
