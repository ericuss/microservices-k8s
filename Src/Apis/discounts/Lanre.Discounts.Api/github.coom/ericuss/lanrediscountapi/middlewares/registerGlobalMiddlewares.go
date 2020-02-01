package middlewares

import (
	"net/http"
)

func SetupGlobalMiddlewares(handler http.Handler) http.Handler {
	healthCheckHandler := useHealthCheck
	handler = healthCheckHandler(handler)
	return handler
}
