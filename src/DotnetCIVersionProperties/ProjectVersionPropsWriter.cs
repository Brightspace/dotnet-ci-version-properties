﻿using System;
using System.IO;

namespace DotnetCIVersionProperties {

	public static class ProjectVersionPropsWriter {

		public static void Write(
				TextWriter output,
				int major,
				int minor,
				int patch,
				string branch,
				string tag,
				int build,
				string sha1
			) {

			string versionPrefix = $"{ major }.{ minor }.{ patch }";

			string versionSuffix;
			if( tag.Equals( $"v{ major }.{ minor }.{ patch }" ) ) {
				versionSuffix = string.Empty;

			} else if( branch.Equals( "master" ) || branch.Equals( "main" ) ) {
				versionSuffix = $"rc.{ build }";

			} else {
				versionSuffix = $"alpha.{ build }";
			}

			string version = versionPrefix;
			if( versionSuffix.Length > 0 ) {
				version += "-" + versionSuffix;
			}

			string assemblyVersion = $"{ major }.{ minor }.0.0";
			string fileVersion = $"{ versionPrefix }.{ build }";
			string informationalVersion = $"{ version }+{ sha1 }";

			output.WriteLine( "<Project>" );
			output.WriteLine( "\t<PropertyGroup>" );
			output.WriteLine( $"\t\t<VersionPrefix>{ versionPrefix }</VersionPrefix>" );
			output.WriteLine( $"\t\t<VersionSuffix>{ versionSuffix }</VersionSuffix>" );
			output.WriteLine( $"\t\t<Version>{ version }</Version>" );
			output.WriteLine( $"\t\t<AssemblyVersion>{ assemblyVersion }</AssemblyVersion>" );
			output.WriteLine( $"\t\t<FileVersion>{ fileVersion }</FileVersion>" );
			output.WriteLine( $"\t\t<InformationalVersion>{ informationalVersion }</InformationalVersion>" );
			output.WriteLine( $"\t\t<RepositoryBranch>{ branch }</RepositoryBranch>" );
			output.WriteLine( $"\t\t<RepositoryCommit>{ sha1 }</RepositoryCommit>" );
			output.WriteLine( "\t</PropertyGroup>" );
			output.WriteLine( "</Project>" );
		}
	}
}
