package router

import (
	"github.com/gorilla/mux"
	"github.com/ericuss/lanrediscountsapi/middlewares"
)

func NewRouter() *mux.Router {
	router := mux.NewRouter().StrictSlash(true)
	router.Use(middlewares.Logger)
	sub := router.PathPrefix("/api").Subrouter()

	for _, route := range routes {
		sub.
			HandleFunc(route.Pattern, route.HandlerFunc).
			Name(route.Name).
			Methods(route.Method)
	}

	return router
}