import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import MainLayout from './core/layouts/MainLayout';
import FeatureFoodManagement from './features/feature-food-management/FeatureFoodManagement';
import { FluentProvider, webLightTheme } from '@fluentui/react-components';
import FeatureMealManagement from './features/feature-meal-management/FeatureMealManagement';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

const router = createBrowserRouter([
  {
    path: '/',
    element: <MainLayout />,
    children: [
      {
        path: 'food',
        element: <FeatureFoodManagement />
      },
      {
        path: 'meal',
        element: <FeatureMealManagement />
      }
    ]
  },
]);

const queryClient = new QueryClient();

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <FluentProvider theme={webLightTheme}>
      <QueryClientProvider client={queryClient}>
        <RouterProvider router={router} />
      </QueryClientProvider>
    </FluentProvider>
  </React.StrictMode>,
)
