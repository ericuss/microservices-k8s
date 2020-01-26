package main

import (
	"log"
	"net/http"
	"github.com/ericuss/lanrediscountsapi/middlewares"
	"github.com/ericuss/lanrediscountsapi/router"
)

func main() {

	router := router.NewRouter()
	log.Fatal(http.ListenAndServe(":8080", middlewares.SetupGlobalMiddlewares(router)))
}