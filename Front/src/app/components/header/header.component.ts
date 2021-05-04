import {AuthService} from '../../services/auth.service';
import {Component} from '@angular/core';
import {BaseService} from 'src/app/services/base.service';
import {ApiUrls} from 'src/app/shared/apiURLs';
import {Router} from '@angular/router';
import {TagModel} from 'src/app/models/tag/tag-model';
import {TagService} from '../../services/tag.service';
import {isNullOrUndefined} from 'util';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
// ntcn
export class HeaderComponent {
  isAuth$ = this.authService.isAuth$;
  tags: TagModel[];

  constructor(
    private authService: AuthService,
    private tagService: TagService,
    private readonly baseService: BaseService,
    private readonly router: Router,
  ) {
  }


  ngOnInit() {


    this.tagService.getTags().subscribe(getPagedTags => 
    {
      if (isNullOrUndefined(getPagedTags)) {
        this.router.navigate(['/']);
        return;
      }

      this.tags = getPagedTags.items;
    });
  }

  getContentByTag(tag: string){
    this.router.navigate(['/'], { queryParams: { tag: tag } });
  }

  userName(){

    return this.authService.getUsername();

  }

  logout() {
    this.baseService.post(ApiUrls.logout)
      .then(() => {
        this.router.navigate(['/', 'login']);
      })
      .finally(() => {
        this.authService.deleteSession();
      });
  }
}
