import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import mkcert from 'vite-plugin-mkcert';

// https://vitejs.dev/config/
export default defineConfig(({ command }) => {
  if (command === 'serve') {
    return {
      // dev specific config
      plugins: [react(), mkcert()],
    };
  } else {
    // command === 'build'
    return {
      // build specific config
    };
  }
});
