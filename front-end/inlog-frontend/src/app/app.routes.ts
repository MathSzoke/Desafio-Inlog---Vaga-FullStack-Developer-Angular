import { Routes } from '@angular/router';
import { VehicleList } from './vehicles/vehicle-list/vehicle-list.component';
import { VehicleCreate } from './vehicles/vehicle-create/vehicle-create.component';

export const routes: Routes = [
  { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
  { path: 'vehicles', component: VehicleList },
  { path: 'vehicles/create', component: VehicleCreate }
];
