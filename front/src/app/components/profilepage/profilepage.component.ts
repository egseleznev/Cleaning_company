import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AuthService} from '../../services/auth/auth.service';

@Component({
  selector: 'app-profilepage',
  templateUrl: './profilepage.component.html',
  styleUrls: ['./profilepage.component.css']
})
export class ProfilepageComponent implements OnInit {
  role=false;
  constructor(private activatedRoute: ActivatedRoute, private _wfServise: AuthService, private route: Router) { }

  ngOnInit(): void {
    this._wfServise.getRole().subscribe(res => {
      if(res=="admin" ){
        this.role=true;
      }
      else { this.role=false;}
    });
  }

}
