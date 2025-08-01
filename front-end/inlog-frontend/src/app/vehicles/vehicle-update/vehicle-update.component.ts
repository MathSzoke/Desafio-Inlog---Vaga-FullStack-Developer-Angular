import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Vehicle } from '../vehicle.service';
import { VehicleFormComponent } from '../vehicle-form/vehicle-form.component';

@Component({
  selector: 'app-vehicle-update',
  standalone: true,
  imports: [VehicleFormComponent],
  templateUrl: './vehicle-update.component.html',
  styleUrls: ['./vehicle-update.component.css']
})
export class VehicleUpdate {
  constructor(
    private dialogRef: MatDialogRef<VehicleUpdate>,
    @Inject(MAT_DIALOG_DATA) public data: { vehicle: Vehicle }
  ) {}

  onSave(vehicle: Partial<Vehicle>) {
    this.dialogRef.close(vehicle);
  }

  onCancel() {
    this.dialogRef.close();
  }
}