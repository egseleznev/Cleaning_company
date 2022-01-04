import { Component, OnInit, Input } from '@angular/core';
import {IUtility, UtilitiesService} from '../../../../services/utilities/utilities.service';
import {ActivatedRoute, Router} from '@angular/router';
import {ISubscriber, ClientsService} from '../../../../services/clients/clients.service';



@Component({
  selector: 'app-cardclient',
  templateUrl: './cardclient.component.html',
  styleUrls: ['./cardclient.component.css']
})
export class CardclientComponent implements OnInit {

  @Input()
  public d:ISubscriber;

  constructor() { }

  ngOnInit(): void {
  }

}
