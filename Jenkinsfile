pipeline {
    agent any 
    stages {
        stage('Push to Heroku registry') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'herokuid', passwordVariable: 'password', usernameVariable: 'username')]) {
                    withEnv(['PATH+HEROKU=C:\\Program Files\\heroku\\bin']) {
                        bat 'heroku container:release web --app=test-api-9'
                    }
                }
            }          
        }

        stage('Cleanup') {
            steps {
                deleteDir()
            }
        }
    }
}
