import {Component, Input} from '@angular/core';
import {IMyEvent} from '../../models/content/i-content';


@Component({
  selector: 'app-content-card',
  templateUrl: './content-card.component.html',
  styleUrls: ['./content-card.component.scss']
})

export class MyEventCardComponent {
  @Input() content: IMyEvent;



}








