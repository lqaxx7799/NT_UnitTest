import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import MainLayout from './core/layouts/MainLayout';
import { FluentProvider, webLightTheme } from '@fluentui/react-components';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

const router = createBrowserRouter([
  {
    path: '/',
    element: <MainLayout />,
    children: [
      {
        path: 'food/create',
        async lazy() {
          let FeatureFoodCreate = await import('./features/feature-food-management/FeatureFoodCreate');
          return { Component: FeatureFoodCreate.default };
        },
      },
      {
        path: 'food',
        async lazy() {
          let FeatureFoodManagement = await import('./features/feature-food-management/FeatureFoodManagement');
          return { Component: FeatureFoodManagement.default };
        },
      },
      {
        path: 'meal',
        async lazy() {
          let FeatureMealManagement = await import('./features/feature-meal-management/FeatureMealManagement');
          return { Component: FeatureMealManagement.default };
        },
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
