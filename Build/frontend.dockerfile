FROM node:14-alpine AS build
WORKDIR /app/src

COPY ./package.json ./package-lock.json ./
RUN npm install

COPY ./ ./
RUN npm run build:prod

FROM nginx:1.19-alpine AS final
COPY --from=build /app/src/dist /usr/share/nginx/html

ENTRYPOINT [ "nginx", "-g", "daemon off;" ]
