import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id: 'dashboards',
        title: 'Dashboards',
        type: 'item',
        icon: 'dashboard',
        url: '/dashboard'
    },
    {
        id: 'users',
        title: 'Users',
        type: 'item',
        icon: 'person',
        url: '/users'
    },
    {
        id: 'newsfeed',
        title: 'News Feed',
        type: 'item',
        icon: 'receipt',
        url: '/newsfeed'
    }

];
