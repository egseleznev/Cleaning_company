import { Component, OnInit , Input, ViewChild, AfterViewInit} from '@angular/core';
import {ISubscriber, ClientsService} from '../../../../services/clients/clients.service';
import {ActivatedRoute, Router} from '@angular/router';
@Component({
  selector: 'app-tableclient',
  templateUrl: './tableclient.component.html',
  styleUrls: ['./tableclient.component.css']
})
export class TableclientComponent implements OnInit {

  displayedColumns: string[] = ['position', 'name', 'weight', 'symbol','utilities'];
  constructor() { }
  @Input() public Clients: ISubscriber[];

  ngOnInit(): void {
  }


}




