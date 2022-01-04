import { Component, OnInit } from '@angular/core';
import {UtilitiesService, IUtility} from '../../services/utilities/utilities.service';
import {ActivatedRoute,Router} from '@angular/router';
import {ClientsService, ISubscriber} from '../../services/clients/clients.service';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {

  iscard=false;
  iseditable=false;
  public Clients:ISubscriber[];
  constructor(private activatedRoute: ActivatedRoute, private _wfServise:ClientsService, private route:Router) { }

  ngOnInit(): void {
    this._wfServise.getClient().subscribe(res=>{this.Clients=res;});
  }

  public AddUtilityToClient(data: { id:number,ids:number }) {
    this._wfServise.addutilitytoclient(JSON.parse(JSON.stringify(data))).subscribe();
    window.location.href='http://localhost:4200/clients'
  }

  public DeleteUtilityFromClient(data: { id:number,ids:number }) {
    this._wfServise.deleteutilityfromclient(JSON.parse(JSON.stringify(data))).subscribe();
    window.location.href='http://localhost:4200/clients'
  }

  onProductEdited() {
    if (this.iseditable==false) {
      this.iseditable=true;
    }
    else this.iseditable=false;
  }
}
