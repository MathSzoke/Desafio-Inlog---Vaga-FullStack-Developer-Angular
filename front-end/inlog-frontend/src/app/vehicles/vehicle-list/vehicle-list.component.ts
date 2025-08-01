import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehicleService, Vehicle } from '../vehicle.service';
import * as L from 'leaflet';
import { provideFluentDesignSystem, fluentButton, fluentTextField, fluentSelect, fluentOption, fluentDataGrid, fluentDataGridRow, fluentDataGridCell, DataGrid } from '@fluentui/web-components';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VehicleCreate } from '../vehicle-create/vehicle-create.component';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { VehicleFormComponent } from '../vehicle-form/vehicle-form.component';
import { MatIconModule } from '@angular/material/icon';
import {VehicleUpdate} from '../vehicle-update/vehicle-update.component';

provideFluentDesignSystem()
  .register(
    fluentButton(),
    fluentTextField(),
    fluentSelect(),
    fluentOption(),
    fluentDataGrid(),
    fluentDataGridRow(),
    fluentDataGridCell()
  );

@Component({
  selector: 'vehicle-list',
  standalone: true,
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css'],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    VehicleCreate,
    VehicleFormComponent,
    MatIconModule,
    VehicleUpdate,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class VehicleList implements OnInit {
  vehicles: Vehicle[] = [];
  map!: L.Map;
  formLat: number | null = null;
  formLon: number | null = null;
  formX: number | null = null;
  formY: number | null = null;
  showForm = false;
  mapExpanded = false;
  hoveringMap = false;

  newVehicle: Partial<Vehicle> = {
    identifier: '',
    chassis: '',
    licensePlate: '',
    trackerSerialNumber: '',
    vehicleType: 1,
    color: '#000000'
  };

  constructor(
    private vehicleService: VehicleService,
    private dialog: MatDialog,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    navigator.geolocation.getCurrentPosition(position => {
      const userLat = position.coords.latitude;
      const userLon = position.coords.longitude;

      this.vehicleService.getVehicles(userLat, userLon).subscribe(data => {
        this.vehicles = data;
        this.updateDataGrid();
        this.initMap(userLat, userLon);
      });
    });
  }

  private updateDataGrid(): void {
    const defaultGridElement = document.getElementById('defaultGrid') as DataGrid;
    if (defaultGridElement) {
      defaultGridElement.rowsData = this.vehicles.map((v, idx) => ({
        "Tipo do veículo": v.vehicleType === 1 ? '🚛 Caminhão' : '🚌 Ônibus',
        Identificador: v.identifier,
        Chassi: v.chassis,
        "Placa do veículo": v.licensePlate,
        "Número de série": v.trackerSerialNumber,
        "Cor do veículo": v.color,
        Latitude: v.coordinates.latitude.toFixed(6),
        Longitude: v.coordinates.longitude.toFixed(6),
        "Ações": `
          <fluent-button 
            appearance="accent"
            class="edit-btn"
            data-idx="${idx}">
            <svg xmlns="http://www.w3.org/2000/svg" height="20" viewBox="0 0 24 24" width="20" fill="#fff">
              <path d="M0 0h24v24H0z" fill="none"/>
              <path d="M3 17.25V21h3.75l11.06-11.06-3.75-3.75L3 17.25zm14.71-9.04c.39-.39.39-1.02 0-1.41l-2.34-2.34a.9959.9959 0 0 0-1.41 0l-1.83 1.83 3.75 3.75 1.83-1.83z"/>
            </svg>
          </fluent-button>
          <fluent-button 
            appearance="accent"
            class="remove-btn danger-btn"
            data-idx="${idx}">
            <svg xmlns="http://www.w3.org/2000/svg" height="20" viewBox="0 0 24 24" width="20" fill="#fff">
              <path d="M0 0h24v24H0z" fill="none"/>
              <path d="M16 9v10H8V9h8m-1.5-6h-5l-1 1H5v2h14V4h-4.5l-1-1zM18 7H6v12c0 1.1.9 2 2 2h8c1.1 0 2-.9 2-2V7z"/>
            </svg>
          </fluent-button>
        `
      }));

      const tryPatchColorCells = () => {
        const rows = defaultGridElement.querySelectorAll('fluent-data-grid-row');
        const allRowsReady = Array.from(rows).every(r => r.children.length === 9);
        if (!allRowsReady) {
          setTimeout(tryPatchColorCells, 50);
          return;
        }
        rows.forEach((row, index) => {
          if (index === 0) return;
          const colorCell = row.children[5] as HTMLElement;
          const colorValue = this.vehicles[index - 1]?.color;
          if (colorCell && colorValue) {
            colorCell.innerHTML = `
              <div style="display: flex; align-items: center; gap: 0.5rem;">
                <div style="
                  width: 1.25rem;
                  height: 1.25rem;
                  border-radius: 4px;
                  border: 1px solid #ccc;
                  background-color: ${colorValue};
                "></div>${colorValue}</div>
            `;
          }
          const actionCell = row.children[8] as HTMLElement;
          if (actionCell) {
            actionCell.innerHTML = `
              <fluent-button 
                appearance="accent"
                class="edit-btn"
                data-idx="${index - 1}">
                <svg xmlns="http://www.w3.org/2000/svg" height="20" viewBox="0 0 24 24" width="20" fill="#fff">
                  <path d="M0 0h24v24H0z" fill="none"/>
                  <path d="M3 17.25V21h3.75l11.06-11.06-3.75-3.75L3 17.25zm14.71-9.04c.39-.39.39-1.02 0-1.41l-2.34-2.34a.9959.9959 0 0 0-1.41 0l-1.83 1.83 3.75 3.75 1.83-1.83z"/>
                </svg>
              </fluent-button>
              <fluent-button 
                appearance="accent"
                class="remove-btn danger-btn"
                data-idx="${index - 1}">
                <svg xmlns="http://www.w3.org/2000/svg" height="20" viewBox="0 0 24 24" width="20" fill="#fff">
                  <path d="M0 0h24v24H0z" fill="none"/>
                  <path d="M16 9v10H8V9h8m-1.5-6h-5l-1 1H5v2h14V4h-4.5l-1-1zM18 7H6v12c0 1.1.9 2 2 2h8c1.1 0 2-.9 2-2V7z"/>
                </svg>
              </fluent-button>
            `;
          }
        });

        defaultGridElement.querySelectorAll('.remove-btn').forEach(btn => {
          btn.replaceWith(btn.cloneNode(true));
        });
        defaultGridElement.querySelectorAll('.edit-btn').forEach(btn => {
          btn.replaceWith(btn.cloneNode(true));
        });
        
        defaultGridElement.querySelectorAll('.remove-btn').forEach(btn => {
          btn.addEventListener('click', (e: any) => {
            const idx = +e.target.getAttribute('data-idx');
            this.onRemoveVehicle(idx);
          });
        });
        defaultGridElement.querySelectorAll('.edit-btn').forEach(btn => {
          btn.addEventListener('click', (e: any) => {
            const idx = +e.target.getAttribute('data-idx');
            this.onEditVehicle(idx);
          });
        });
      };
      setTimeout(tryPatchColorCells, 0);
    }
  }

  onEditVehicle(idx: number) {
    const vehicle = this.vehicles[idx];
    if (!vehicle?.id) {
      this.toastr.error('ID do veículo não encontrado.');
      return;
    }
    const dialogRef = this.dialog.open(VehicleUpdate, {
      width: '600px',
      data: { vehicle: { ...vehicle } }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.vehicleService.updateVehicle(result).subscribe({
          next: () => {
            this.vehicles[idx] = result;
            this.updateDataGrid();
            this.toastr.success('Veículo atualizado com sucesso!');
          },
          error: (err) => {
            const msg = err?.error?.detail || err?.error?.title || 'Erro ao atualizar veículo.';
            this.toastr.error(msg);
          }
        });
      }
    });
  }

  onRemoveVehicle(idx: number) {
    const vehicle = this.vehicles[idx];
    if (!vehicle?.id) {
      this.toastr.error('ID do veículo não encontrado.');
      return;
    }
    if (!confirm(`Tem certeza que deseja remover o veículo ${vehicle.identifier}?`)) return;
    this.vehicleService.deleteVehicle(vehicle.id).subscribe({
      next: () => {
        this.vehicles.splice(idx, 1);
        this.updateDataGrid();
        this.toastr.success('Veículo removido com sucesso!');
      },
      error: (err) => {
        console.log(err);
        const msg = err?.error?.detail || err?.error?.title || 'Erro ao remover veículo.';
        this.toastr.error(msg);
        this.toastr.error('Erro ao remover o veículo.');
      }
    });
  }

  initMap(userLat: number, userLon: number): void {
    this.map = L.map('map').setView([userLat, userLon], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: 'Map data © <a href="https://openstreetmap.org">OpenStreetMap</a> contributors'
    }).addTo(this.map);

    this.vehicles.forEach(vehicle => {
      const icon = this.getVehicleIcon(vehicle.vehicleType, vehicle.color);
      L.marker([vehicle.coordinates.latitude, vehicle.coordinates.longitude], { icon })
        .addTo(this.map)
        .bindPopup(`
          <span>Chassi: <b>${vehicle.chassis}</b></span><br/>
          <span>Identificador: <b>${vehicle.identifier}</b></span><br/>
          <span>Placa do veículo: <b>${vehicle.licensePlate}</b></span><br/>
          <span>Numero do rastreador: <b>${vehicle.trackerSerialNumber}</b></span>
        `, { offset: L.point(0, -30) });
    });

    this.map.on('click', (e: L.LeafletMouseEvent) => {
      const containerPoint = this.map.latLngToContainerPoint(e.latlng);
      this.formLat = e.latlng.lat;
      this.formLon = e.latlng.lng;
      this.formX = containerPoint.x;
      this.formY = containerPoint.y;
      this.newVehicle = {
        identifier: '',
        chassis: '',
        licensePlate: '',
        trackerSerialNumber: '',
        vehicleType: 1,
        color: '#000000',
        coordinates: {
          latitude: e.latlng.lat,
          longitude: e.latlng.lng
        }
      };
      this.showForm = true;
    });

    this.map.on('movestart', () => {
      this.showForm = false;
    });

    this.map.invalidateSize();
  }

  getVehicleIcon(type: number, color: string): L.DivIcon {
    const emoji = type === 1 ? '🚛' : '🚌';
    const html = `
      <div style="
        width: 36px;
        height: 48px;
        background: ${color};
        clip-path: path('M18 0 C28 0, 36 10, 36 20 C36 30, 28 40, 18 48 C8 40, 0 30, 0 20 C0 10, 8 0, 18 0 Z');
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 20px;
        color: white;
      ">${emoji}</div>
    `;
    return L.divIcon({ html, className: '', iconSize: [36, 48], iconAnchor: [18, 48] });
  }

  saveVehicle(): void {
    if (!this.newVehicle.coordinates) return;
    const vehicle: Vehicle = {
      ...this.newVehicle,
      vehicleType: Number(this.newVehicle.vehicleType),
      coordinates: {
        latitude: this.newVehicle.coordinates.latitude!,
        longitude: this.newVehicle.coordinates.longitude!
      }
    } as Vehicle;

    this.vehicleService.createVehicle(vehicle).subscribe({
      next: () => {
        const icon = this.getVehicleIcon(vehicle.vehicleType, vehicle.color);
        L.marker([vehicle.coordinates.latitude, vehicle.coordinates.longitude], { icon })
          .addTo(this.map)
          .bindPopup(`
            <b>${vehicle.chassis}</b><br/>
            <b>${vehicle.identifier}</b><br/>
            <b>${vehicle.licensePlate}</b><br/>
            <b>${vehicle.trackerSerialNumber}</b>
          `, { offset: L.point(0, -20) });
        this.vehicles.push(vehicle);
        this.updateDataGrid();
        this.showForm = false;
        this.toastr.success('Veículo cadastrado com sucesso!');
      },
      error: (err) => {
        const msg = err?.error?.detail || err?.error?.title || 'Erro ao cadastrar veículo.';
        this.toastr.error(msg);
      }
    });
  }

  cancelForm(): void {
    this.showForm = false;
  }

  toggleMap(event?: MouseEvent) {
    if (event) event.stopPropagation();
    this.mapExpanded = !this.mapExpanded;
    if (this.map) {
      setTimeout(() => this.map.invalidateSize(), 300);
    }
  }

  openCreateModal(): void {
    const dialogRef = this.dialog.open(VehicleCreate, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe((result: Vehicle | undefined) => {
      if (result) {
        this.vehicles.push(result);
        this.updateDataGrid();
        if (this.map) {
          const icon = this.getVehicleIcon(result.vehicleType, result.color);
          L.marker([result.coordinates.latitude, result.coordinates.longitude], { icon })
            .addTo(this.map)
            .bindPopup(`
              <b>${result.chassis}</b><br/>
              <b>${result.identifier}</b><br/>
              <b>${result.licensePlate}</b><br/>
              <b>${result.trackerSerialNumber}</b>
            `, { offset: L.point(0, -20) });
        }
      }
    });
  }
}