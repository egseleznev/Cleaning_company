import { Component, OnInit } from '@angular/core';
import {UtilitiesService, IUtility} from '../../services/utilities/utilities.service';
import {ActivatedRoute,Router} from '@angular/router';
@Component({
  selector: 'app-utilities',
  templateUrl: './utilities.component.html',
  styleUrls: ['./utilities.component.css']
})
export class UtilitiesComponent implements OnInit {

  istable = false;
  iseditable = false;

  public Utilities: IUtility[];
  public stat: number[];

  constructor(private activatedRoute: ActivatedRoute, private _wfServise: UtilitiesService, private route: Router) {
  }

  public Add(data: { name: string, price:number,isfavourite: boolean }) {
    this._wfServise.addutility(JSON.parse(JSON.stringify(data))).subscribe();
    window.location.href='http://localhost:4200/utility'
  }

  public Edit(data: {id:number,name: string, price:number,isfavourite: boolean }) {
    this._wfServise.pututility(JSON.parse(JSON.stringify(data))).subscribe();
    window.location.href='http://localhost:4200/utility'
  }

  public Delete(data: {id:number}) {
    this._wfServise.deleteutility(JSON.parse(JSON.stringify(data))).subscribe();
    window.location.href='http://localhost:4200/utility'
  }

  ngOnInit(): void {
    this._wfServise.getUtility().subscribe(res => {
      this.Utilities = res;
    });
    this._wfServise.getUtilityStat().subscribe(res =>{
      this.stat = res;
    });
  }

  onProductEdited() {
      if (this.iseditable==false) {
        this.iseditable=true;
      }
      else this.iseditable=false;
  }

}
