import { Component, OnInit, Input } from '@angular/core';
import {IUtility, UtilitiesService} from '../../../../services/utilities/utilities.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-utility',
  templateUrl: './utility.component.html',
  styleUrls: ['./utility.component.css']
})
export class UtilityComponent implements OnInit {

  @Input()
  public f:IUtility;

  constructor() { }

  ngOnInit(): void {
  }

}
