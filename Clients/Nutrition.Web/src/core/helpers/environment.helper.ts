export type Environment = {
  baseUrlApi: string;
};

const environmentKeyMap: { [key in keyof Environment]: string } = {
  baseUrlApi: 'VITE_BASE_API_URL',
};

const getEnvironmentVariable = (key: keyof Environment): string => {
  return import.meta.env[environmentKeyMap[key]];
};

const getEnvironmentVariables = (): { [key in keyof Environment]: string } => {
  return Object.keys(environmentKeyMap).reduce<{ [key: string]: string }>((res, key) => {
    res[key] = getEnvironmentVariable( key as keyof Environment);
    return res;
  }, {}) as { [key in keyof Environment]: string };
};

const EnvironmentHelpers = {
  getEnvironmentVariable,
  getEnvironmentVariables,
};

export default EnvironmentHelpers;
