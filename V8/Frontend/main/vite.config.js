// vite.config.js
import { defineConfig } from 'vite';
import { environment } from '@environment/environment';

export default defineConfig(environment.isAspire?
  {
  base: '/main/',   
  server: {
    // This entire 'server' object only contains the HMR fix you need.
    hmr: {
      protocol: 'ws', 
      host: 'localhost',
      port: 5043, // The port of your Aspire/YARP proxy
      path: '/main', // The path the proxy forwards to HMR
    },
  },
}
: 
{}
);