import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehicleService, Vehicle } from '../vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef } from '@angular/material/dialog';
import { VehicleFormComponent } from '../vehicle-form/vehicle-form.component';

@Component({
  selector: 'vehicle-create',
  standalone: true,
  templateUrl: './vehicle-create.component.html',
  styleUrls: ['./vehicle-create.component.css'],
  imports: [CommonModule, VehicleFormComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class VehicleCreate {
  newVehicle: Partial<Vehicle> = {
    identifier: '',
    chassis: '',
    licensePlate: '',
    trackerSerialNumber: '',
    color: '#000000',
    vehicleType: 1,
    coordinates: { latitude: 0, longitude: 0 }
  };
  submitted = false;

  constructor(
    private vehicleService: VehicleService,
    private toastr: ToastrService,
    private dialogRef: MatDialogRef<VehicleCreate>
  ) {}

  cancelForm() {
    this.dialogRef.close();
  }

  saveVehicle(vehicle: Partial<Vehicle>) {
    this.submitted = true;
    const newVehicle: Vehicle = {
      identifier: vehicle.identifier!,
      chassis: vehicle.chassis!,
      licensePlate: vehicle.licensePlate!,
      trackerSerialNumber: vehicle.trackerSerialNumber!,
      color: vehicle.color!,
      vehicleType: Number(vehicle.vehicleType),
      coordinates: {
        latitude: parseFloat((vehicle.coordinates?.latitude ?? "0").toString()),
        longitude: parseFloat((vehicle.coordinates?.longitude ?? "0").toString()),
      }
    };

    this.vehicleService.createVehicle(newVehicle).subscribe({
      next: (createdVehicle) => {
        this.toastr.success('Veículo cadastrado com sucesso!');
        this.dialogRef.close(createdVehicle);
      },
      error: (err) => {
        const msg = err?.error?.detail || err?.error?.title || 'Erro ao cadastrar veículo.';
        this.toastr.error(msg);
      }
    });
  }
}