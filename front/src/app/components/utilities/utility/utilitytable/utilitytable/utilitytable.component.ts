import { Component, OnInit , Input, ViewChild, AfterViewInit} from '@angular/core';
import {IUtility, UtilitiesService} from '../../../../../services/utilities/utilities.service';
import {ActivatedRoute, Router} from '@angular/router';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';

@Component({
  selector: 'app-utilitytable',
  templateUrl: './utilitytable.component.html',
  styleUrls: ['./utilitytable.component.css']
})


export class UtilitytableComponent implements AfterViewInit {
  displayedColumns: string[] = ['id', 'name', 'price', 'isfavourite'];
  dataSource: MatTableDataSource<IUtility>;

  @Input() public Utilities: IUtility[];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor() {
    this.dataSource = new MatTableDataSource(this.Utilities);

  }
  ngAfterViewInit(): void {
    console.log(this.Utilities);
    console.log(this.Utilities?.length);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    console.log(this.Utilities);
    console.log(this.Utilities?.length);
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}




