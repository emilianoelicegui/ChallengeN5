// import { createStore, combineReducers } from 'redux';
// import permissionReducer from './reducers/permissionReducer';

// const rootReducer = combineReducers({
//   permissions: permissionReducer,
// });

// const store = createStore(rootReducer);

// export default store;

import { configureStore } from '@reduxjs/toolkit';
import rootReducer from '../reducers/';

const store = configureStore({
  reducer: rootReducer,
  // Otras configuraciones de la tienda
});

export default store;