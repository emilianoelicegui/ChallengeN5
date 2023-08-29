import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import './index.css'

import store from './store/store';
import { Provider } from 'react-redux'
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { ThemeProvider, createTheme } from '@mui/material';

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
  },
});

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Provider store={store}>
    <LocalizationProvider dateAdapter={AdapterDateFns}>
    <ThemeProvider theme={darkTheme}>
        <App />
        </ThemeProvider>
      </LocalizationProvider>
    </Provider>
  </React.StrictMode>
);